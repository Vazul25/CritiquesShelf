import { Book } from './Book';

export interface UserBooks {
	favourites: Book[];
	maxFavouritesCount: number;
	reviewed: Book[];
	maxReviewedCount: number;
	read: Book[];
	maxReadCount: number;
	likeToRead: Book[];
	maxLikeToReadCount: number;
} 