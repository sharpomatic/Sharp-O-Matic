import type {ReactNode} from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import HomepageFeatures from '@site/src/components/HomepageFeatures';
import Heading from '@theme/Heading';

import styles from './index.module.css';
import heroImage from '@site/src/assets/hero.png';
import heroLogo from '@site/static/img/large_light.png';

function HomepageHeader() {
  const {siteConfig} = useDocusaurusContext();
  return (
    <header className={styles.hero}>
      <div className={styles.heroInner}>
        <div className={styles.heroContent}>
          <div className={styles.heroTitleRow}>
            <div className={styles.heroLogoWrap}>
              <img
                className={styles.heroLogo}
                src={heroLogo}
                alt="SharpOMatic logo"
              />
            </div>
            <div className={styles.heroTitleText}>
              <Heading as="h1" className={styles.heroTitle}>
                {siteConfig.title}
              </Heading>
              <p className={styles.heroSubtitle}>{siteConfig.tagline}</p>
            </div>
          </div>
        </div>
        <img
          className={styles.heroImage}
          src={heroImage}
          alt="SharpOMatic screenshot"
        />
      </div>
    </header>
  );
}

export default function Home(): ReactNode {
  const {siteConfig} = useDocusaurusContext();
  return (
    <Layout
      title={siteConfig.tagline}
      description="SharpOMatic is a workflow designer and execution engine for AI tasks in .NET.">
      <div className={styles.page}>
        <HomepageHeader />
        <main>
          <HomepageFeatures />
        </main>
      </div>
    </Layout>
  );
}
