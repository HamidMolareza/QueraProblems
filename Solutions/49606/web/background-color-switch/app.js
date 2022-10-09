const buttons = document.getElementsByClassName("button");
for (i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", e => {
        console.log(e.target.id);
        document.body.style.backgroundColor = e.target.id;
    })
}
