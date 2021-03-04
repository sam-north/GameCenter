import { Component, OnInit } from '@angular/core';
import { CRUDExample } from 'src/app/models/CRUDExample';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'crud-examples',
  templateUrl: './crud-examples.component.html',
  styleUrls: ['./crud-examples.component.scss']
})
export class CrudExamplesComponent implements OnInit {

  crudExamples: CRUDExample[] = []; 

  constructor(private god: GodService) { }

  async ngOnInit(): Promise<void> {
    await this.load();
  }  

  async load() {
    this.crudExamples = await this.god.api.getCrudExamples();
  }

  create(){
    this.god.router.navigate(['/crud-example', 0]);
  }

  edit(crudExample: CRUDExample){
    this.god.router.navigate(['/crud-example', crudExample.id]);
  }

  async delete(crudExample: CRUDExample){
    await this.god.api.deleteCrudExample(crudExample.id);
    await this.load();
    this.god.notifications.success('deleted');
  }
}
