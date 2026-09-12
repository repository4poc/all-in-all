import { Card, CardContent } from '@/components/ui/card';
import { ProductType } from '@/utils/products';
import Image from 'next/image';

export function CardImage({ product }: { product: ProductType }) {
  return (
    <Card className='w-80 h-80 bg-cyan-950 dark:bg-muted text-white'>
      <CardContent className='p-2'>
        <div className='relative h-70 md:h-48 rounded overflow-hidden '>
          <Image
            src={product.url}
            alt=''
            fill
            priority
            className='rounded w-full object-cover transform group-hover:scale-110 transition-transform duration-500'
          />
        </div>
        <div className='mt-4 text-center'>
          <h2 className='text-lg font-normal capitalize'>{product.name}</h2>
          <p className=' mt-2'>{product.price}</p>
        </div>
      </CardContent>
    </Card>
  );
}
