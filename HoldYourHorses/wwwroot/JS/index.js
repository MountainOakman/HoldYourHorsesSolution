
const fromSlider = document.querySelector("#fromSlider");
const toSlider = document.querySelector("#toSlider");
const fromInput = document.querySelector("#fromInput");
const toInput = document.querySelector("#toInput");
const fromSliderHK = document.querySelector("#fromSliderHK");
const toSliderHK = document.querySelector("#toSliderHK");
const fromInputHK = document.querySelector("#fromInputHK");
const toInputHK = document.querySelector("#toInputHK");
const allTypes = document.querySelectorAll(".kategori");
const allMaterials = document.querySelectorAll(".material");
const selectElement = document.querySelector('#sort');

var maxPrice = toInput.value;

var minPrice = fromInput.value;
var maxHK = toInputHK.value;
var minHK = fromInputHK.value;
var sortOn = "Pris";
var isAscending = true;
var typer = "-";
var materials = "-"
var searchString = "";

//event listeners
allTypes.forEach(o => {
    typer += " " + o.value;
    o.addEventListener("change", function () {
        if (this.checked) {
            typer += " " + this.value;
            getPartialView()
        }
        else {
            typer = typer.replace(" " + this.value, "");
            getPartialView()
        }
    })
});
allMaterials.forEach(o => {
    materials += " " + o.value;
    o.addEventListener("change", function () {
        if (this.checked) {
            materials += " " + this.value;
            getPartialView()
        }
        else {
            materials = materials.replace(" " + this.value, "");
            getPartialView()
        }
    })
});
const constMaxPrice = maxPrice;
const constMinPrice = minPrice;
const constSortOn = sortOn;
const constMaxHK = maxHK
const constMinHK = minHK
const constIsAscending = isAscending
const constMaterials = materials
const constTyper = typer
selectElement.addEventListener('change', (event) => {
    sortOn = event.target.value.slice(1);
    isAscending = Boolean(parseInt(event.target.value.substr(0, 1)));
    getPartialView();
});
//price  event listener
fromSlider.addEventListener("change", (event) => { 
    minPrice = event.target.value;
    getPartialView();}
);
toSlider.addEventListener("change", (event) => {
    maxPrice = event.target.value;
    getPartialView();
});
fromInput.addEventListener("change", (event) => { 
    minPrice = event.target.value
    getPartialView();
}
);
toInput.addEventListener("change", (event) => {
    maxPrice = event.target.value;
    getPartialView();
});
//Hästkrafter event listener
fromSliderHK.addEventListener("change", (event) => {
    minHK = event.target.value;
    getPartialView();
}
);
toSliderHK.addEventListener("change", (event) => {
    maxHK = event.target.value;
    getPartialView();
});
fromInputHK.addEventListener("change", (event) => {
    minHK = event.target.value;
    getPartialView();
}
);
toInputHK.addEventListener("change", (event) => {
    maxHK = event.target.value;
    getPartialView();
});


///script starts here
getPartialView();




//functions
async function getPartialView() {
    const superContainer = document.querySelector(".card-container");
    await fetch(`/IndexPartial/?maxPrice=${maxPrice}&minPrice=${minPrice}&maxHK=${maxHK}&minHK=${minHK}&typer=${typer}&materials=${materials}&sortOn=${sortOn}&isAscending=${isAscending}`, { method: "GET" }).
        then(result => result.text()).
        then(html => {
            superContainer.innerHTML = html;
        });
}

async function resetFilter() {
    {
        toSlider.value = constMaxPrice;
        fromSlider.value = constMinPrice;
        toSliderHK.value = constMaxHK;
        fromSliderHK.value = constMinHK;
        toInput.value = constMaxPrice;
        fromInput.value = constMinPrice;
        toInputHK.value = constMaxHK;
        fromInputHK.value = constMinHK;
        


        controlFromSlider(fromSlider, toSlider, fromInput)
        controlToSlider(fromSlider, toSlider, toInput);
        controlFromInput(fromSlider, fromInput, toInput, toSlider);
        controlToInput(toSlider, fromInput, toInput, toSlider);
        controlFromSlider(fromSliderHK, toSliderHK, fromInputHK);
        controlToSlider(fromSliderHK, toSliderHK, toInputHK);
        controlFromInput(fromSliderHK, fromInputHK, toInputHK, toSliderHK);
        controlToInput(toSliderHK, fromInputHK, toInputHK, toSliderHK);
}

    allTypes.forEach(o => {
        o.checked = true;
    });
    allMaterials.forEach(o => o.checked = true);

    maxPrice = constMaxPrice;
    minPrice = constMinPrice;
    sortOn = constSortOn;
    maxHK = constMaxHK;
    minHK = constMinHK;
    isAscending = constIsAscending;
    materials = constMaterials;
    typer = constTyper;
    getPartialView();
}





///// Slider JAvascript code /////
function controlFromInput(fromSlider, fromInput, toInput, controlSlider) {
    const [from, to] = getParsed(fromInput, toInput);
    fillSlider(fromInput, toInput, "#C6C6C6", "#25daa5", controlSlider);
    if (from > to) {
        fromSlider.value = to;
        fromInput.value = to;
    } else {
        fromSlider.value = from;
    }
}

function controlToInput(toSlider, fromInput, toInput, controlSlider) {
    const [from, to] = getParsed(fromInput, toInput);
    fillSlider(fromInput, toInput, "#C6C6C6", "#25daa5", controlSlider);
    setToggleAccessible(toInput);
    if (from <= to) {
        toSlider.value = to;
        toInput.value = to;
    } else {
        toInput.value = from;
    }
}

function controlFromSlider(fromSlider, toSlider, fromInput) {
    const [from, to] = getParsed(fromSlider, toSlider);
    fillSlider(fromSlider, toSlider, "#C6C6C6", "#25daa5", toSlider);
    if (from > to) {
        fromSlider.value = to;
        fromInput.value = to;
    } else {
        fromInput.value = from;
    }
}

function controlToSlider(fromSlider, toSlider, toInput) {
    const [from, to] = getParsed(fromSlider, toSlider);
    fillSlider(fromSlider, toSlider, "#C6C6C6", "#25daa5", toSlider);
    setToggleAccessible(toSlider);
    if (from <= to) {
        toSlider.value = to;
        toInput.value = to;
    } else {
        toInput.value = from;
        toSlider.value = from;
    }
}

function getParsed(currentFrom, currentTo) {
    const from = parseInt(currentFrom.value, 10);
    const to = parseInt(currentTo.value, 10);
    return [from, to];
}

function fillSlider(from, to, sliderColor, rangeColor, controlSlider) {
    const rangeDistance = to.max - to.min;
    const fromPosition = from.value - to.min;
    const toPosition = to.value - to.min;
    controlSlider.style.background = `linear-gradient(
      to right,
      ${sliderColor} 0%,
      ${sliderColor} ${(fromPosition / rangeDistance) * 100}%,
      ${rangeColor} ${(fromPosition / rangeDistance) * 100}%,
      ${rangeColor} ${(toPosition / rangeDistance) * 100}%, 
      ${sliderColor} ${(toPosition / rangeDistance) * 100}%, 
      ${sliderColor} 100%)`;
}

function setToggleAccessible(currentTarget) {
    const toSlider = document.querySelector("#toSlider");
    if (Number(currentTarget.value) <= 0) {
        toSlider.style.zIndex = 2;
    } else {
        toSlider.style.zIndex = 0;
    }
}


fillSlider(fromSlider, toSlider, "#C6C6C6", "#25daa5", toSlider);
setToggleAccessible(toSlider);

fillSlider(fromSliderHK, toSliderHK, "#C6C6C6", "#25daa5", toSliderHK);
setToggleAccessible(toSliderHK);

fromSlider.oninput = () => controlFromSlider(fromSlider, toSlider, fromInput);
toSlider.oninput = () => controlToSlider(fromSlider, toSlider, toInput);
fromInput.oninput = () =>
    controlFromInput(fromSlider, fromInput, toInput, toSlider);
toInput.oninput = () => controlToInput(toSlider, fromInput, toInput, toSlider);

fromSliderHK.oninput = () =>
    controlFromSlider(fromSliderHK, toSliderHK, fromInputHK);
toSliderHK.oninput = () => controlToSlider(fromSliderHK, toSliderHK, toInputHK);
fromInputHK.oninput = () =>
    controlFromInput(fromSliderHK, fromInputHK, toInputHK, toSliderHK);
toInputHK.oninput = () =>
    controlToInput(toSliderHK, fromInputHK, toInputHK, toSliderHK);


var isShown = true;
function showHideFilter(){
    const filter = document.querySelector(".filter");
    const listItems = filter.children;
    const listArray = [...listItems];
    listArray.shift();
    const svg = document.querySelector("#hamburger");
    console.log(svg)
    if (isShown) {
        for (var i = 0; i < listArray.length; i++) {
            listArray[i].style.display = "none";
        }
        isShown = false;
        console.log(filter);
        filter.style.minWidth = "6rem";
        filter.style.border = "0px solid black";
        filter.style.background = "white";
        svg.style.transform = 'rotate(0)';
        
    }
    else {
        isShown = true;
        filter.style.minWidth = "35rem";
        filter.style.border = "3px solid black";
        filter.style.borderTop = "none";
        filter.style.background = "#c6e6f5"
        svg.style.transform = 'rotate(90deg)';

        for (var i = 0; i < listArray.length; i++) {
            setTimeout(function (a) { a.style.display = "block" }, 300, listArray[i]);
            console.log(listArray[i]);
        }
        
    } 
    }


function hideProperty(id) {
    if (id.style.height != "0px") {
        id.style.height = "0"
    }
    else {
        id.style.height = "auto"
    }
}