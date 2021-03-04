export class Response<T> {
    constructor() {    

    }
    errors: string[];
    messages: string[];
    isValid: boolean;
    data: T;
  }