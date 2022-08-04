
function UppdateraVarukorg() {
    const pris = document.getElementById('price').innerHTML
    const varor = document.getElementById('AntalVaror').value
    const artikel = document.getElementById('artikelnr').innerHTML
    const namn = document.getElementById('artikelnamn').innerHTML
    fetch(`/uppdateravarukorg/?artikelnr=${artikel}&antalvaror=${varor}&price=${pris}&artikelnamn=${namn}`,
        {
            method: "GET",
        });
}


