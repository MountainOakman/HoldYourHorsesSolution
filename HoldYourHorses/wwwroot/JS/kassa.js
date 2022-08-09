function deleteItem(artikelnr, pris, antal) {
    fetch(`/deleteItem/?artikelnr=${artikelnr}`,
        {
            method: "GET",
        }).then(o => {
            artikelContainer = document.querySelector(".artikel-container");
            var artikel = document.getElementById(artikelnr);
            artikelContainer.removeChild(artikel);
            var totalsumma = document.getElementById('totalsumma');
            totalsumma.innerHTML = parseInt(totalsumma.innerHTML.replace(" ", "")) - pris * antal + " kr";
            console.log(totalsumma.innerHTML);
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
        })
        .then(o => {
            var totalsumma = document.getElementById('totalsumma');
            totalsumma.innerHTML = "Din kundvagn är tom"
            var summa = document.getElementById('summa');
            summa.innerHTML = "";
            var artikelContainer = document.getElementById('container');
            artikelContainer.innerHTML = "";
            const numberofproducts = document.getElementById('number-of-products');
            numberofproducts.attributes[2].value = 0;
            console.log(numberofproducts)
        })
}
function checkout() {
    window.location = "/checkout";
}
function ShowOrHideButtons() {
    const numberofproducts = document.getElementById('number-of-products');
    console.log(numberofproducts)
    if (numberofproducts.attributes[2].value == 0) {
        document.querySelector("#rensakorg").style.display = "none";
        document.querySelector("#betalning").style.display = "none";
    }
}

kundvagn();
ShowOrHideButtons();