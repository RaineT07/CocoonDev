let height;
let width;
let canvas;
let ctx;
let mousePos = { x: undefined, y: undefined };
let x;
let y;
let randomQuestions = [];

let questionText;
let answersText = [];
let nextButton;
let currentQuestion = 1;


let usersAnswers=[];

const init = () => {
    // Fetch from external file
    fetch('questions.json')
        .then(response => {
            if (!response.ok) {
                throw new Error('could not fetch data');
            }
            return response.json();
        })
        .then(data => {
            // Store questions and answers in a variable
            const questions = data.questions;
            randomQuestions = getRandomElements(questions, 3);
            randomQuestions.unshift("Press next to start");
            console.log(randomQuestions);
            // Initialize canvas and other elements
            initCanvas();
        })
        .catch(error => {
            console.error('could not fetch data:', error);
        });
}

// Initialize canvas and mosuemove event
const initCanvas = () => {
    // Canvas setup
    canvas = document.querySelector('.myCanvas');
    ctx = canvas.getContext('2d');
    width = canvas.width;
    height = canvas.height;

    //elements setup
    nextButton = document.querySelector('#next-button');
    questionText = document.querySelector('#question-text');
    answersText = document.querySelectorAll('.overlay');

    // Log the answers text for debugging purposes
    console.log(answersText);

    // Display initial question and answers
    questionText.innerHTML = randomQuestions[0];
    

    // Event listeners
    canvas.addEventListener('mousemove', (e) => {
        mousePos = getMousePos(canvas, e);
        x = mousePos.x;
        y = mousePos.y;

        console.log(x, y);
    });
    nextButton.addEventListener('click', () => {
        newQuestion();
    });
}

//get mouse position
const getMousePos = (canvas, evt) => {
    const rect = canvas.getBoundingClientRect();
    return {
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    };
};


const newQuestion = () => {
    if (currentQuestion < randomQuestions.length) {
        usersAnswers.push([x, y]);
        questionText.innerHTML = randomQuestions[currentQuestion].question;
        for (let i = 0; i < 4; i++) {
            answersText[i].innerHTML = randomQuestions[currentQuestion].answers[i];
        }
        currentQuestion++;
    } else {
        console.log(usersAnswers);
    }
};

// shuffles input array and selects a number of elements
const getRandomElements = (arr, num) => {
    const shuffled = arr.sort(() => 0.5 - Math.random());
    return shuffled.slice(0, num);
}

window.onload = init;
