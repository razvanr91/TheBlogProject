﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBlogProject.Data;
using TheBlogProject.Models;
using TheBlogProject.Services;

namespace TheBlogProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ISlugService _slugService;

        private readonly IImageService _imageService;

        public PostsController(ApplicationDbContext context, ISlugService slugService, IImageService imageService)
        {
            _context = context;
            _slugService = slugService;
            _imageService = imageService;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.Author).Include(p => p.Blog);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if ( id == null )
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if ( post == null )
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Abstract,Content,ReadyStatus,Image")] Post post, List<string> tagValues)
        {
            if ( ModelState.IsValid )
            {
                post.Created = DateTime.Now;

                // Use image service to store the image
                post.ImageData = await _imageService.EncodeImageAsync(post.Image);
                post.ContentType = _imageService.ContentType(post.Image);
                
                

                var slug = _slugService.UrlFriendly(post.Title);

                // If title is not unique
                if(!_slugService.IsUnique(slug))
                {
                    // Add Model state error and return user to create view
                    ModelState.AddModelError("Title", "The title you provided is not unique. Please find another one.");
                    ViewData["TagValues"] = string.Join(",", tagValues);
                    return View(post);
                }

                post.Slug = slug;

                _context.Add(post);

                foreach(var tagText in tagValues)
                {
                    _context.Tags.Add(new Tag()
                    {
                        PostId = post.Id,
                        AuthorId = post.AuthorId,
                        Text = tagText
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null )
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if ( post == null )
            {
                return NotFound();
            }

            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Name", post.BlogId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogId,Title,Abstract,Content,ReadyStatus")] Post post, IFormFile newImage)
        {
            if ( id != post.Id )
            {
                return NotFound();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    var editedPost = await _context.Posts.FindAsync(post.Id);
                    editedPost.Updated = DateTime.Now;

                    if(newImage is not null)
                    {
                        editedPost.ImageData = await _imageService.EncodeImageAsync(newImage);
                        editedPost.ContentType = _imageService.ContentType(newImage);
                    }

                    if ( post.Title != editedPost.Title )
                        editedPost.Title = post.Title;

                    if ( post.Abstract != editedPost.Abstract )
                        editedPost.Abstract = post.Abstract;

                    if ( post.Content != editedPost.Content )
                        editedPost.Content = post.Content;

                    if ( post.ReadyStatus != editedPost.ReadyStatus )
                        editedPost.ReadyStatus = post.ReadyStatus;

                    await _context.SaveChangesAsync();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !PostExists(post.Id) )
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", post.AuthorId);
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Description", post.BlogId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null )
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if ( post == null )
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
