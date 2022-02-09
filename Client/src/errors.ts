// Collection of in-jokes
const messages = [
    "i am inside your walls.",
    "i am rapidly approaching your location.",
    "smonk detor.",
    "fix it!!!!",
    "mad because bad.",
    "remain upset.",
    "you are sooo irate."
]

// Returns a random message from messages
export function getErrorMessage() {
    return messages[Math.floor(Math.random() * messages.length)];
}