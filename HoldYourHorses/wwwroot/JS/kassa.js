﻿function deleteItem(artikelnr, pris, antal) {
    fetch(`/deleteItem/?artikelnr=${artikelnr}`,
        {
            method: "GET",
        }).then(o => {
           
            var artikel = document.getElementById(artikelnr);
            artikelContainer.removeChild(artikel);
            var totalsumma = document.getElementById('totalsumma');
            totalsumma.innerHTML = parseInt(totalsumma.innerHTML) - pris * antal + " kr";
            kundvagn();
            const numberofproducts = document.getElementById('number-of-products');
            numberofproducts.attributes[2].value = parseInt(numberofproducts.attributes[2].value) - antal;
        })
}
function kundvagn() {
    var rensakorg = document.getElementById('rensakorg')
    var totalsumma = document.getElementById('totalsumma');
    if (totalsumma.innerHTML == "0 kr") {
        totalsumma.innerHTML = "Din kundvagn är tom"
        var summa = document.getElementById('summa');
        var betalning = document.getElementById('betalning');
        betalning.style.display = "none";
        rensakorg.style.display = "none";
        summa.innerHTML = "";

    };
}

function rensakorg() {
    fetch("/rensakorg",
        {
            method: "GET",
        });
        //.then(o => {
        //    var artikel = document.getElementById('container').getElementsByClassName('artikel');
        //    artikel = "";
        //})
}
function checkout(){
    window.location = "/checkout";
   }
kundvagn()