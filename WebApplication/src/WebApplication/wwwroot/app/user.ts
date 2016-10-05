export class User {
    id: number;
    chat_s: string;
    Person: string;

    constructor(id: number,
        chat_s: string,
        Person: string) {
        this.id = id;
        this.chat_s = chat_s;
        this.Person = Person;
    }
}
