import Image from 'next/image';
import LandingZone from '@/public/images/CustomLandingZone.png';

export default function page() {
  return (
    <div>
      <p>
        This demo showcases a production-ready Azure Landing Zone built using
        Terraform and a multi-region Hub-Spoke network architecture. The
        solution establishes a governed cloud foundation with centralized
        networking, security, and policy management across multiple Azure
        regions. The architecture includes dedicated Hub Virtual Networks
        hosting shared services such as Azure Firewall, VPN/ExpressRoute
        Gateway, and Azure Bastion, while Spoke Virtual Networks host
        application workloads segmented into web and backend tiers. Regional
        deployments are connected through VNet peering to enable secure and
        scalable connectivity while maintaining workload isolation. Governance
        is enforced through Azure Policy, including location restrictions,
        approved resource types, and mandatory tagging standards. The landing
        zone is designed to support enterprise-scale deployments, improve
        operational consistency, strengthen security controls, and accelerate
        cloud adoption through Infrastructure as Code (IaC) using Terraform.
      </p>
      <b>Key capabilities include:</b>
      <ul>
        <li>
          Multi-region deployment strategy for resilience and scalability.
        </li>
        <li>
          Hub-Spoke network topology with centralized security and connectivity
        </li>
        <li>Azure Policy-based governance and compliance enforcement.</li>
        <li>
          Standardized resource organization using dedicated resource groups.
        </li>
        <li>
          Secure administration through Azure Bastion and controlled network
          access.
        </li>
        <li>
          Infrastructure provisioning and lifecycle management with Terraform.
        </li>
        <li>
          This architecture provides a secure, scalable, and governed foundation
          for deploying enterprise applications on Azure.
        </li>
      </ul>

      <Image src={LandingZone} alt='Landing Zone'></Image>
    </div>
  );
}
