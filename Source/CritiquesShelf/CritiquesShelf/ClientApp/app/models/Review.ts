﻿export interface Review {
	id: number;
	userId: string;
    userName: string;
    description: string;
    date: Date; 
    score: number;
    bookId: number;
    bookTitle: string;
}