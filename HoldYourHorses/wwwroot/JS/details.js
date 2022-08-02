function UppdateraVarukorg(id) {
    fetch("json/{ArtikelNr}",
        {
            method: "Get",
        })
        .then(result => result.json())
        .then(obj => function UppdateraKorg('${ArtikelNr}+{AntalVaror}') );
}