using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CodeSparks.Controllers
{
    public class BackgroundController : Controller
    {
        private readonly Random _random = new Random();

        [HttpGet("background.svg")]
        public ContentResult GetRandomBackgroundSvg()
        {
            var svgWidth = 1920; // Set your desired SVG width
            var svgHeight = 1080; // Set your desired SVG height

            var svgBuilder = new StringBuilder();
            svgBuilder.AppendLine($"<svg xmlns='http://www.w3.org/2000/svg' width='{svgWidth}' height='{svgHeight}'>");

            for (int i = 0; i < 30; i++) // Adjust the number of stars as needed
            {
                var star = GenerateStar(svgWidth, svgHeight);
                svgBuilder.AppendLine(star);
            }

            svgBuilder.AppendLine("</svg>");

            return Content(svgBuilder.ToString(), "image/svg+xml");
        }

        private string GenerateStar(int svgWidth, int svgHeight)
        {
            int cx = _random.Next(0, svgWidth);
            int cy = _random.Next(0, svgHeight);
            int outerRadius = _random.Next(5, 15);
            int innerRadius = outerRadius / 2;

            var points = new StringBuilder();
            double rot = _random.NextDouble() * 360; // Add random rotation
            for (int i = 0; i < 10; i++) // 10 points for a 5-pointed star
            {
                double angle = i * 36 * Math.PI / 180 + rot; // Apply rotation
                double r = (i & 1) == 0 ? outerRadius : innerRadius; // Alternate between radii

                double x = cx + r * Math.Cos(angle);
                double y = cy - r * Math.Sin(angle);

                points.Append($"{x},{y} ");
            }

            var color = string.Format("#{0:X6}", _random.Next(0x1000000));
            return $"<polygon points='{points}' fill='{color}' />";
        }
    }
}
