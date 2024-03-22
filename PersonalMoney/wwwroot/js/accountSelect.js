
const selectWallet = () => {
    localStorage.setItem("walletId", document.getElementById('layout__slcWallet').value);
}
function onloadWallet() {
    var selectElement = document.getElementById("layout__slcWallet");
    var selectedValue = localStorage.getItem("walletId");
    console.log("selectedValue", selectedValue)
    for (var i = 0; i < selectElement.options.length; i++) {
        option = selectElement.options[i];
        console.log("value", option.val)

        if (option.value === selectedValue) {
            option.setAttribute("selected", true);
        }

    }
}
$('#layout__slcWallet').on('change', () => {
    localStorage.setItem("walletId", document.getElementById('layout__slcWallet').value);
});
onloadWallet();