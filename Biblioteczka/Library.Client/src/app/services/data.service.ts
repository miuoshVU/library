 
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
 
@Injectable({
  providedIn: 'root'
})
export class DataService {
  subjectNotifier: Subject<null> = new Subject<null>();
 
  constructor() { }
 
  notifyAboutChange() {
    this.subjectNotifier.next(null);
  }
}
 