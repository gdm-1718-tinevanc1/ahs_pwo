export class Iproject {
    public id?: number | null;
    public title: string;
    public shorttitle: string;
    public startdate: Date;
    public enddate: Date;
    public subtitle: string;
    public description: string;
    public abstract: string;
    public statusId: number;
    public status:  Array<any>;
    public partnerValidate: string;
    public participantValidate: string;
    public financingformValidate: string;
    public linkValidate: string;
    public budgetValidate: string;
    public participants: Array<any>;
    public budget: Array<any>;
    public publications: Array<any>;
    // public spearheads: Array<any>;
    public tags: Array<any>;
    public partners: Array<any>;
    public links: Array<any>;
    public profiles: Array<any>;
    public profileId: number | null;
    public mediums: Array<any>;
    public financingforms: Array<any>;

}
