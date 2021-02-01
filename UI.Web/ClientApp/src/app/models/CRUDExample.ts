export class CRUDExample {
    constructor(){
        this.id = 0;
        this.text= null;
        this.dateCreated=  null;
        this.dateDeleted=  null;
        this.isDeleted=  false;
    }

    id: number;
    text: string;
    dateCreated: Date;
    isDeleted: boolean;
    dateDeleted: Date;
}