import { Demo } from '@/utils/demos';
import { Separator } from '@/components/ui/separator';
import Link from 'next/link';

type DemosListProps = {
  demos: Demo[];
};

export default function DemosList({ demos }: DemosListProps) {
  return (
    <div className='grid grid-cols-1 md:grid-cols-4 gap-10 mt-8'>
      {demos.map((demo) => {
        return (
          <Link key={demo.id} href={demo.href}>
            <div
              className='bg-white rounded-2xl shadow-lg hover:shadow-2xl transition-transform hover:-translate-y-2 p-8 flex flex-col items-center text-center'
              style={{ opacity: '1', transform: 'none' }}
              key={demo.id}
            >
              <div className='bg-gradient-to-r from-green-400 to-blue-400 text-white rounded-full p-4 mb-4 shadow-lg'>
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  className='h-10 w-10'
                  fill='none'
                  viewBox='0 0 24 24'
                  stroke='currentColor'
                >
                  <circle cx='12' cy='12' r='10' strokeWidth='2'></circle>
                  <path d='M2 12h20M12 2v20' strokeWidth='2'></path>
                </svg>
              </div>
              <h2 className='font-semibold mb-3 dark:text-black'>
                {demo.title}
              </h2>
              <p className='text-gray-600 text-sm text-left'>
                {demo.description}
              </p>
              <Separator className='mt-4' />
              <>
                <p className='w-full pl-1 mt-4 text-black text-sm font-bold text-left'>
                  Technology Stack
                </p>
                <ul className='w-full list-disc pl-5 text-black text-sm text-left'>
                  {demo.techstack?.split(',').map((tech) => {
                    return <li key={tech}>{tech}</li>;
                  })}
                </ul>
              </>
            </div>
          </Link>
        );
      })}
    </div>
  );
}
