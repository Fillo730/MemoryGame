export function scrollToTop () : void {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

export function scrollToBottom() : void {
    window.scrollTo({
        top: document.body.scrollHeight,
        behavior: 'smooth'
    });
}