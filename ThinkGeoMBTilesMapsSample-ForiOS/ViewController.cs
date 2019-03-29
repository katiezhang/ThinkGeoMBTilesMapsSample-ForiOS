using CoreGraphics;
using System;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.iOS;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using UIKit;

namespace ThinkGeoMBTilesMapsSample_ForiOS
{
    public partial class ViewController : UIViewController
    {
        private MapView mapView;
        private LayerOverlay layerOverlay;
        private ThinkGeoMBTilesFeatureLayer thinkGeoMBTilesFeatureLayer;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            this.mapView = new MapView(new CoreGraphics.CGRect(new CGPoint(View.Frame.Left, View.Frame.Top), new CGSize(View.Frame.Width, View.Frame.Height)))
            {
                MapUnit = GeographyUnit.Meter
            };
            View.AddSubview(this.mapView);

            this.thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("AppData/tiles_Frisco.mbtiles", new Uri("AppData/thinkgeo-world-streets-light.json", UriKind.Relative));
            this.thinkGeoMBTilesFeatureLayer.BitmapTileCache.ClearCache();
            this.layerOverlay = new LayerOverlay
            {
                MaxExtent = this.thinkGeoMBTilesFeatureLayer.GetTileMatrixBoundingBox(),
                TileHeight = 512,
                TileWidth = 512,
                TileSnappingMode = TileSnappingMode.Snapping
            };
            this.layerOverlay.Layers.Add(this.thinkGeoMBTilesFeatureLayer);
            this.mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet(512);
            this.mapView.Overlays.Add(this.layerOverlay);
            this.mapView.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);

            this.mapView.CurrentExtentChanged += MapView_CurrentExtentChanged;

            this.mapView.Refresh();
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            RectangleShape currentExtent = ((MapView)sender).CurrentExtent;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}