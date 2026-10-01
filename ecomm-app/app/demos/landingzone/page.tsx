import Image from 'next/image';
import LandingZone from '@/public/images/CustomLandingZone.png';

export default function page() {
  return (
    <>
      <div className='space-y-3 py-3'>
        <section className='rounded-lg border border-amber-200 bg-amber-50 p-4'>
          <h2 className='mb-2 text-lg font-semibold text-amber-800'>
            Azure Landing Zone
          </h2>
          <p className='leading-relaxed text-slate-700'>
            This showcases a production-ready Azure Landing Zone built with
            Terraform and a multi-region hub-and-spoke network architecture. It
            provides a governed foundation for deploying and managing cloud
            workloads across Azure regions.
          </p>
        </section>
        <div className='flex flex-wrap gap-4'>
          <a
            href='/request-demo'
            className='inline-flex items-center rounded-lg bg-blue-600 px-6 py-3 text-sm font-semibold text-white shadow-sm transition hover:bg-blue-700'
          >
            Request a Demo
          </a>

          <a
            href='https://github.com/repository4poc/all-in-all/tree/main/azure_landing_zone'
            target='_blank'
            rel='noopener noreferrer'
            className='inline-flex items-center rounded-lg border border-gray-300 bg-white px-6 py-3 text-sm font-semibold text-gray-900 transition hover:bg-gray-50'
          >
            View on GitHub
          </a>
        </div>
        <Image src={LandingZone} alt='Landing Zone'></Image>

        <section className='rounded-lg border border-amber-200 bg-amber-50 p-4'>
          <h3 className='mb-2 text-lg font-semibold text-amber-800'>
            Centralized governance and networking
          </h3>
          <p className='leading-relaxed text-slate-700'>
            The solution centralizes networking, security, and policy
            management. Dedicated hub virtual networks host shared services,
            including Azure Firewall, VPN or ExpressRoute Gateway, and Azure
            Bastion.
          </p>
        </section>

        <section className='rounded-lg border border-amber-200 bg-amber-50 p-4'>
          <h3 className='mb-2 text-lg font-semibold text-amber-800'>
            Segmented application workloads
          </h3>
          <p className='leading-relaxed text-slate-700'>
            Spoke virtual networks host application workloads, with separate web
            and backend subnets to support workload isolation and controlled
            traffic flows.
          </p>
        </section>

        <section className='rounded-lg border border-amber-200 bg-amber-50 p-4'>
          <h3 className='mb-2 text-lg font-semibold text-amber-800'>
            Secure, multi-region connectivity
          </h3>
          <p className='leading-relaxed text-slate-700'>
            Regional deployments connect through virtual network peering,
            enabling scalable communication while preserving workload
            boundaries.
          </p>
        </section>

        <section className='rounded-lg border border-amber-200 bg-amber-50 p-4'>
          <h3 className='mb-2 text-lg font-semibold text-amber-800'>
            Policy-driven compliance
          </h3>
          <p className='leading-relaxed text-slate-700'>
            Azure Policy helps enforce approved deployment locations, permitted
            resource types, and mandatory tagging standards.
          </p>
        </section>

        <section className='rounded-lg border border-amber-200 bg-amber-50 p-4'>
          <h3 className='mb-2 text-lg font-semibold text-amber-800'>
            Built for enterprise scale
          </h3>
          <p className='leading-relaxed text-slate-700'>
            Managed as Infrastructure as Code with Terraform, this landing zone
            promotes operational consistency, strengthens security controls, and
            supports a scalable approach to cloud adoption.
          </p>
        </section>
      </div>
    </>
  );
}
