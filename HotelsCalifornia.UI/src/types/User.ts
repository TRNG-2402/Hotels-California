export interface Manager 
{   
    id : number,
    username : string,
    passwordHash : string,
    hotelId : number
}

export interface Member
{
    id : number,
    username : string,
    passwordHash : string,
    LicenseNumber: string,
    Email : string,
    phoneNumber : string,
    rewardPoints : number,
    inBlockList: boolean,
}

export interface NewManagerDTO
{
    username: string,
    passwordHash: string,   
    hotelId: number
}
export interface NewMemberDTO
{
    username : string,
    passwordHash : string,
    licenseNumber: string,
    email : string,
    phoneNumber : string
}

export interface NewAdminDTO
{
    username : string,
    passwordHash : string
}

export interface Admin
{
    username : string,
    passwordHash : string
}

export interface UpdateUserDTO
{
    id: number,
    username: string,
    passwordHash: string
}
