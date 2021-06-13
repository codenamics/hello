import { item } from "./item";

export interface list{
    id: string;
    title: string;
    order?: number,
    items: item[]
}