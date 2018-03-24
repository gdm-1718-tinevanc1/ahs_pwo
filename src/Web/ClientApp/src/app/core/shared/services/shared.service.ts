import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class SharedService {

  sharedNode = {
    valueObject: {
      nl: {
        value: '',
        validate: {
          allow_changes:  false,
          required: true,
          publication_ok: false,
          validated_by: null,
          feedback: '',
          editable_by: [],
          visible_to: null,
          updated_at: Date
        }
      },
      en: {
        value: '',
        validate: {
          allow_changes:  false,
          required: true,
          publication_ok: false,
          validated_by: null,
          feedback: '',
          editable_by: [],
          visible_to: null,
          updated_at: Date
        }
      }
    },
    validateObject: {
      validate: {
        allow_changes:  false,
        required: true,
        publication_ok: false,
        validated_by: null,
        feedback: '',
        editable_by: [],
        visible_to: null,
        updated_at: Date
      }
    }  
  };

  public language: BehaviorSubject<string> = new BehaviorSubject<string>(null);


  setLanguage(value: string) {
    this.language.next(value);
  }
  
}


