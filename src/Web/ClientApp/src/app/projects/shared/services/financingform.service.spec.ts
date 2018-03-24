import { TestBed, inject } from '@angular/core/testing';

import { FinancingformService } from './financingform.service';

describe('FinancingformService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FinancingformService]
    });
  });

  it('should be created', inject([FinancingformService], (service: FinancingformService) => {
    expect(service).toBeTruthy();
  }));
});
