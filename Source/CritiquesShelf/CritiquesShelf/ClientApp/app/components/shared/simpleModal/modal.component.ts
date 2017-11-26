import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
    selector: 'simpleModal', 
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css'],
})
export class SimpleModalComponent {
    title: string;
    message:string;
    constructor(
        public dialogRef: MatDialogRef<SimpleModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {

        this.title = data.title;
        this.message = data.message;

    }
    onNoClick(): void {
        
        this.dialogRef.close();
    }

}

 
