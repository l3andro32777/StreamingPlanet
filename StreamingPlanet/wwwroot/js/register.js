//https://codepen.io/coding_beast/pen/LYGrXde
let name = document.getElementById("Input_Name");
let email = document.getElementById("Input_Email");
let mailFormat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

name.addEventListener("keyup", function () {
    if (name.value.length >= 2) {
        name.classList.add("valid");
    } else {
        name.classList.remove("valid");
    }
});

email.addEventListener("keyup", function () {
    if (email.value.match(mailFormat)) {
        email.classList.add("valid");
    } else {
        email.classList.remove("valid");
    }
});
