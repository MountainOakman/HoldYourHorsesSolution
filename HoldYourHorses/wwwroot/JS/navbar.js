function searchFunction() {
    searchString = document.getElementById("search-input").value;
    console.log(searchString);
    window.location.href = `/?search=${searchString}`;
}