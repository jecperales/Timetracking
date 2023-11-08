import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.pissa.timetracking',
  appName: 'PissaTimetracking',
  webDir: 'www',
  server: {
    androidScheme: 'https'
  }
};

export default config;
