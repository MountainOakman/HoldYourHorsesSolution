function deleteItem(artikelnr) {
    fetch(`/deleteItem/?artikelnr=${artikelnr}`,
        {
            method: "GET",
        }).then(o => {
            var artikelContainer = document.getElementsByClassName("artikel-container");
            var artikel = document.getElementById(artikelnr);
            artikelContainer.removeChild(artikel);
        })
}