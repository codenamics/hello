import { item } from "./item/item";

export interface list{
    id: string;
    title: string;
    order?: number,
    cards: item[]
}