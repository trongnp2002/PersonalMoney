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


document.querySelectorAll('.delete-link').forEach(function (link) {
    link.addEventListener('click', function (event) {
        // Ngăn chặn hành động mặc định của liên kết
        event.preventDefault();

        // Hiển thị hộp thoại xác nhận
        if (confirm('Are you sure you want to delete this debtor?')) {
            // Nếu người dùng ấn OK, chuyển hướng đến đường dẫn được chỉ định trong thuộc tính href của liên kết
            window.location.href = link.getAttribute('href');
        } else {
            // Nếu người dùng ấn Cancel, không thực hiện hành động nào
        }
    });
});




document.getElementById("filterType").onchange = function () {
    var filterType = this.value;
    var rangeContainer = document.getElementById("rangeContainer");
    var filterValueStart = document.getElementById("filterValueStart");
    var filterValueEnd = document.getElementById("filterValueEnd");
    var errorSpan = document.getElementById("errorSpan");
    var searchButton = document.getElementById("searchButton");

    if (filterType === "total") {
        filterValueEnd.style.display = "block";
        filterValueStart.type = "number";
        filterValueEnd.type = "number";
        filterValueStart.placeholder = "From";
        filterValueStart.classList.remove("error");
        filterValueEnd.classList.remove("error");
        errorSpan.textContent = "";
        errorSpan.style.display = "none";
    } else if (filterType === "date") {
        filterValueEnd.style.display = "block";
        filterValueStart.type = "date";
        filterValueEnd.type = "date";
        errorSpan.style.display = "none";
    }
    else if (filterType === "name") {
        filterValueStart.type = "text";
        filterValueStart.placeholder = "Enter name";
        filterValueEnd.style.display = "none";
        errorSpan.style.display = "none";
    }
    filterValueStart.oninput = function () {
        var startValue = filterValueStart.value;
        var endValue = filterValueEnd.value;

        if (filterType === "total" && parseFloat(startValue) > parseFloat(endValue)) {
            errorSpan.textContent = "Start value must be less than end value";
            errorSpan.style.display = "block";
            filterValueStart.classList.add("error");
            filterValueEnd.classList.add("error");
        } else if (filterType === "date" && startValue > endValue) {
            errorSpan.textContent = "Start date must be before end date";
            errorSpan.style.display = "block";
            filterValueStart.classList.add("error");
            filterValueEnd.classList.add("error");
        } else {
            errorSpan.textContent = "";
            errorSpan.style.display = "none";
            filterValueStart.classList.remove("error");
            filterValueEnd.classList.remove("error");
        }
    };

    filterValueEnd.oninput = function () {
        var startValue = filterValueStart.value;
        var endValue = filterValueEnd.value;

        if (filterType === "total" && parseFloat(startValue) > parseFloat(endValue)) {
            errorSpan.textContent = "Start value must be less than end value";
            errorSpan.style.display = "block";
            filterValueStart.classList.add("error");
            filterValueEnd.classList.add("error");
        } else if (filterType === "date" && startValue > endValue) {
            errorSpan.textContent = "Start date must be before end date";
            errorSpan.style.display = "block";
            filterValueStart.classList.add("error");
            filterValueEnd.classList.add("error");
        } else {
            errorSpan.textContent = "";
            errorSpan.style.display = "none";
            filterValueStart.classList.remove("error");
            filterValueEnd.classList.remove("error");
        }
    };

};


document.addEventListener('DOMContentLoaded', function () {
    var clearButton = document.getElementById('clearButton');
    clearButton.addEventListener('click', function () {
        // Xử lý sự kiện khi người dùng nhấp vào nút Clear Filter
        document.getElementById('filterForm').reset();
        document.getElementById('rangeContainer').classList.remove('active'); // Bỏ chọn active class cho rangeContainer (nếu có)
        document.getElementById("filterValueStart").value = "";
        document.getElementById("filterValueEnd").value = "";
        var debtorId = document.getElementById("debtorId").value;
        // window.location.href = '/Debtor/Detail/view-detail/' + debtorId;

    });
});


