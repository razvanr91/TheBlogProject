let index = 0;

function AddTag() {
    let tagEntry = document.getElementById("TagEntry");

    let newOption = new Option(tagEntry.value, tagEntry.value);

    document.getElementById("TagValues").options[index++] = newOption;

    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagValues = document.getElementById("TagValues");
    let selectedTagIndex = tagValues.selectedIndex;
    tagValues.options.DeleteTag(selectedTagIndex);
}