export interface Book {
    id: number;
    title: string;
    authorsNames: string[];
    description: string;
    Tags: string[];
    rateing: number;
    datePublished: number;
    favourite: boolean;
    likeToRead: boolean;
    read: boolean;
    cover: string;
}