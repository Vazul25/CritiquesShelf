import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { NgForOf, NgClass } from '@angular/common';

@Component({
    selector: 'stars',
    templateUrl: './stars.component.html',
    styleUrls: ['./stars.component.css']
})
export class StarsComponent {
    @Input() rating: number;
    @Input() readOnly: boolean = false;

  	@Output() ratingChange: EventEmitter<number> = new EventEmitter<number>();

  	ngOnInit() {
  		// Round to nearest 0.5: Example: 1.1 --> 1.0 ; 1.6 --> 1.5
  		this.rating = (Math.round(this.rating * 2) / 2);
  	}

    onClick(value: number) {
    	if (!this.readOnly) {
			this.rating = value;
    		this.ratingChange.emit(this.rating);
    	}
    }
}