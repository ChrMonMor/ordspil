function inputJumpingOnMax(x, y) {
    if (y.length == x.maxLength) {
        var next = x.tabIndex+1;
        if (next < document.getElementById("GuessForm").length) {
            document.getElementById("GuessForm").elements[next].focus();
        }
    }
}