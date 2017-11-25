import { Review } from "./Review";
export interface BookDetails {
    id: number;
    title: string;
    authorsNames: string[];
    description: string;
    Tags: string[];
    rateing: number;
    datePublished: number;
    favouriteCount: number;
  
    cover: string;
    reviews: Review[];
} 
 
        
         
        
        
        
      

        
      