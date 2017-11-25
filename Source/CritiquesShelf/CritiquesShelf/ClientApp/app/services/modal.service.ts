import { Injectable, Inject, OnInit, EventEmitter } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { SimpleModalComponent } from '../components/shared/simpleModal/modal.component';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';


@Injectable()
export class ModalService {

    constructor(public dialog: MatDialog) {

    }


    openSimpleMessageDialog(title: string,message:string): void {


        let dialogRef = this.dialog.open(SimpleModalComponent, {
            width: '450px',
            
            data: { message: message, title:title }
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log('The dialog was closed');
        });

    }
}