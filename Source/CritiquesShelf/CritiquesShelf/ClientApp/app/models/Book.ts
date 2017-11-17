export interface Book {

    title: string;
    authorName: string;
    description: string;
    datePublished: Date;
    Tags: Set<string>;

}