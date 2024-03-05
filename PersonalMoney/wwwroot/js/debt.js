function selectOptions(isPlus) {
    var plusOption = document.getElementById("plus");
    var minusOption = document.getElementById("minus");
    var classifyValue = document.getElementById("classifyValue");

    if (isPlus) {
        plusOption.classList.add("active");
        minusOption.classList.remove("active");
        classifyValue.value = "true";
    } else {
        plusOption.classList.remove("active");
        minusOption.classList.add("active");
        classifyValue.value = "false";
    }
}

function selectOption(isPlus, id) {
    var plusOption = document.getElementById("plus" + id);
    var minusOption = document.getElementById("minus" + id);
    var classifyValue = document.getElementById("classifyValue" + id);

    if (isPlus) {
        plusOption.classList.add("active");
        minusOption.classList.remove("active");
        classifyValue.value = "true";
    } else {
        plusOption.classList.remove("active");
        minusOption.classList.add("active");
        classifyValue.value = "false";
    }
}
