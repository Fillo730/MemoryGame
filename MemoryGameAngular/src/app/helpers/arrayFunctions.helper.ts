export function shuffleArray<T>(array : T[]) : T[] {
    return array.map((item) => ({ item, sort: Math.random() }))
        .sort((a, b) => a.sort - b.sort)
        .map(({ item }) => item);
}

export function sampleArray<T>(array: T[], n : number) : T[] {
    const size = n > array.length ? array.length : n;

    const shuffledArray = shuffleArray<T>(array);

    return shuffledArray.slice(0, size);
}

export function isLastElement<T>(array: T[], element : T) : boolean {
    if(array.length == 0) return false;
    return array.at(array.length-1) === element;
}