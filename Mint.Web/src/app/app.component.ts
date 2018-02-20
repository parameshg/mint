import { Component, TemplateRef } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'mint-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'mint';

  constructor(private modal: BsModalService) {
  }

  open(dialog: TemplateRef<any>) {
    this.modal.show(dialog);
  }
}
