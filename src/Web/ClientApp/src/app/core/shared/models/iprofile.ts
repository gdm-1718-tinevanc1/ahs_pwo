export class Iprofile {
    public id?: number | null;
    public userName: string;
    public firstName: string;
    public lastName: string;
    public email: string;
    public employeenumber: number;
    public setting: {
        id?: number | null;
        language: string;
        color: string;
    }

}
