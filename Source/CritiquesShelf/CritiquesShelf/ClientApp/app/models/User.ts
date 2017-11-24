import { Review } from './Review';
import { ReadingStat } from './ReadingStat';
export interface User {
	id: string;
    email: string;
    firstName: string;
    lastName: string;
    photo: any;
    reviews: Review[];
    readingStat: ReadingStat;
} 