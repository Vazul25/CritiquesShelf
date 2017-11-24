export interface Book {
    id: number;
    title: string;
    authorsNames: string[];
    description: string;
    Tags: string[];
    rateing: number;
    datePublished: number;
    coverSource: string;
    favourite: boolean;
    likeToRead: boolean;
    read: boolean;
}