let index = 0;

function AddTag() {
    let tagEntry = document.getElementById("TagEntry");

    let newOption = new Option(tagEntry.value, tagEntry.value);

    document.getElementById("TagValues").options[index] = newOption;
    index++;
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    while (tagCount > 0) {
        let tagValues = document.getElementById("TagValues");
        let selectedTagIndex = tagValues.selectedIndex;
        if (selectedTagIndex >= 0) {
            tagValues.options[selectedTagIndex] = null;
            --tagCount;
        } else {
            tagCount = 0;
        }
        if(index > 0)
            index--;
    }
}

$("form").on("submit", function () {
    $("#TagValues option").prop("selected", "selected");
})