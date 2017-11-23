import { Review } from './Review';
export interface User {
    email: string;
    firstName: string;
    lastName: string;
    photo: any;
    reviews: Review[];
} 