import { Icategory } from "./icategory";

export interface CategoryWithChildren {
    category: Icategory;
    children: Icategory[] | null;
}
