function UppdateraVarukorg(varor, artikel, namn, pris) {
    fetch(`/uppdateravarukorg/?artikelnr=${artikel}&antalvaror=${varor}&pris=${pris}&artikelnamn=${namn}`,
        {
            method: "GET",
        });
}


