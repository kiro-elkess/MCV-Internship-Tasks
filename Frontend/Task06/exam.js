let a = 5;
let b = 10;
[a, b] = [b, a];
console.log("Task 1:", a, b);

let numbers = [4, 9, 2, 7, 5];
let max = Math.max(...numbers);
console.log("Task 2:", max);

let str = "JavaScript is awesome";
let vowelsCount = (str.match(/[aeiou]/gi) || []).length;
console.log("Task 3:", vowelsCount);

function isPrime(num) {
    if (num <= 1) return false;
    for (let i = 2; i <= Math.sqrt(num); i++) {
        if (num % i === 0) return false;
    }
    return true;
}
console.log("Task 4:", isPrime(17));

function reverseString(s) {
    return s.split('').reverse().join('');
}
console.log("Task 5:", reverseString("hello"));

let nums = [1, 2, 3, 4, 5, 6];
let sumEven = nums.reduce((sum, num) => num % 2 === 0 ? sum + num : sum, 0);
console.log("Task 6:", sumEven);

let arr = [1, 2, 3, 2, 4, 1, 5];
let uniqueArr = [...new Set(arr)];
console.log("Task 7:", uniqueArr);

console.log("Task 8:");
for (let i = 1; i <= 30; i++) {
    if (i % 15 === 0) console.log("FizzBuzz");
    else if (i % 3 === 0) console.log("Fizz");
    else if (i % 5 === 0) console.log("Buzz");
    else console.log(i);
}

function factorial(n) {
    if (n === 0 || n === 1) return 1;
    return n * factorial(n - 1);
}
console.log("Task 9:", factorial(5));

let car = { brand: "Toyota", model: "Corolla", year: 2020, color: "blue" };
console.log("Task 10:");
for (let key in car) {
    console.log(`${key}: ${car[key]}`);
}
