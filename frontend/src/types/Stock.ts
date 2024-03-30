export type StockServerType = {
  id: number;
  stockName: string;
  basePrice: number;
  supplyQty: number;
  supplyPrice: number;
  demandQty: number;
  demandPrice: number;
  lastPrice: number;
  updateTime: Date;
};

export type StockClientType = StockServerType & {
  totalSupply: number;
  totalDemand: number;
  percentageChange: number;
  isPercentageChangePositive?: boolean;
};
