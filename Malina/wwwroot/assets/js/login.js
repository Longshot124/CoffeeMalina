let status = document.getElementById("status");
let contents = document.getElementById("pass");
let usercontents = document.getElementById("user");
var l = 0;
var show = true;
var show_button = document.getElementById("show_button");
var background = document.getElementById("background");
var mockPage = document.getElementById("page");

console.clear();

//window.onload = () => {};

const sleep = (delay) => new Promise((resolve) => setTimeout(resolve, delay));

async function check() {
    if (contents.value === "1234" && usercontents.value === "user1234") {
        console.log("correct!");
        l = 0;
        openPage();
        status.innerHTML = "";
    } else {
        console.log("incorrect password!");
        l++;
        status.innerHTML = "Incorrect Password or Username!";
        status.style.color = "var(--red)";
        screenShake();
        lock();
        waitOnPress();
    }
}

function lock() {
    if (l > 2) {
        console.log("too many incorrect attempts!");
        document.getElementById("button").disabled = true;
        document.getElementById("button").style.color = "white";
        status.innerHTML = "Too many attempts! Try again later.";
    }
}

contents.onkeydown = function (e) {
    // If enter key pressed then enter input
    if (e.keyCode == 13 && l < 3) {
        check();
    }
};

async function reset() {
    console.clear();
    console.log("login attempts reset, try again :)");
    l = 0;
    status.innerHTML = "";
    document.getElementById("button").disabled = false;
    document.getElementById("button").style.color = "white";
    await sleep(1000);
    console.clear();
}

function showStuff() {
    if (show) {
        contents.type = "input";
        show_button.innerHTML = "Hide Password";
        show = false;
    } else {
        contents.type = "password";
        show_button.innerHTML = "Show Password";
        show = true;
    }
}

async function screenShake() {
    container.style.animation = "wiggle 0.5s ease-in-out";
    await sleep(500);
    container.style.animation = "";
}

async function waitOnPress() {
    if (l < 3) {
        document.getElementById("button").disabled = true;
        await sleep(500);
        document.getElementById("button").disabled = false;
    } else {
        document.getElementById("button").disabled = true;
    }
}

function openPage() {
    mockPage.style.display = "block";
}

async function back() {
    mockPage.style.animation = "slideout 1s ease-in-out";
    await sleep(1000);
    mockPage.style.display = "none";
    mockPage.style.animation = "";
}
