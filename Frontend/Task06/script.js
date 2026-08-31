const mysteriousMessages = [
    "A journey of a thousand miles begins with a single line of code.",
    "You will encounter an unexpected token today.",
    "Great refactoring is in your near future.",
    "An old deprecated feature will soon be forgotten.",
    "The answer lies deep within the documentation."
];

const buttonElement = document.getElementById('fortuneBtn');
const textElement = document.getElementById('fortuneText');

buttonElement.addEventListener('click', function() {
    const randomIndex = Math.floor(Math.random() * mysteriousMessages.length);
    textElement.textContent = mysteriousMessages[randomIndex];
});
