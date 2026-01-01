import type {ReactNode} from 'react';
import Heading from '@theme/Heading';
import styles from './styles.module.css';

type BenefitItem = {
  title: string;
  description: ReactNode;
};

const BenefitList: BenefitItem[] = [
  {
    title: 'Iterate Fast, Ship Faster',
    description: (
      <>
        Iterate faster to explore ideas and refine your workflows using
        configuration instead of code.
      </>
    ),
  },
  {
    title: 'Deep Integration',
    description: (
      <>
        Write workflow logic in C# and call your backend code directly. 
        Reuse your existing code instead of using another language.
      </>
    ),
  },
  {
    title: 'Easy Setup',
    description: (
      <>
        Be up and working within minutes. Download nuget packages and add 
        services to your program setup.
      </>
    ),
  },
];

function Benefit({title, description}: BenefitItem) {
  return (
    <div className={styles.benefitCard}>
      <Heading as="h3" className={styles.benefitTitle}>
        {title}
      </Heading>
      <p className={styles.benefitDescription}>{description}</p>
    </div>
  );
}

export default function HomepageFeatures(): ReactNode {
  return (
    <section className={styles.benefits}>
      <div className={styles.benefitsGrid}>
        {BenefitList.map((benefit) => (
          <Benefit key={benefit.title} {...benefit} />
        ))}
      </div>
    </section>
  );
}
