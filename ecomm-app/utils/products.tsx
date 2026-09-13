export type ProductType = {
  id: string;
  name: string;
  price: string;
  url: string;
};

const productsArray: ProductType[] = [
  {
    id: '1',
    name: 'Wall stand',
    price: '200',
    url: '/images/product-1.jpg',
  },
  {
    id: '2',
    name: 'Comfort Bed',
    price: '300',
    url: '/images/product-2.jpg',
  },
  {
    id: '3',
    name: 'King sofa',
    price: '400',
    url: '/images/product-3.jpg',
  },
  {
    id: '4',
    name: 'Mini Sofa',
    price: '200',
    url: '/images/product-4.jpg',
  },
];

export default productsArray;
